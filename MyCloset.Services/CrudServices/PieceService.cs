﻿using Auth.Abstractions;
using Microsoft.AspNetCore.Http;
using MyCloset.Domain.Entities;
using MyCloset.Domain.Models;
using MyCloset.Infra.Abstractions.QueryFilters;
using MyCloset.Infra.Abstractions.Repositories;
using MyCloset.Infra.File;
using MyCloset.Services.Abstractions.CrudServices;
using MyCloset.Services.Seed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Util.Extensions;
using Util.Services;

namespace MyCloset.Services.CrudServices
{
	public class PieceService : CrudService<Piece, IPieceQueryFilter>, IPieceService
	{
		IFiles Files { get; }
		ITagService TagService { get; }
		IContextTools ContextTools { get; }
		Account LoggedUser { get; }

		public PieceService
		(
			IPieces pieces, 
			IFiles files,
			ITagService tagService,
			IContextTools contextTools,
			IAccountProvider accountProvider
		) : base(pieces, contextTools)
		{
			Files = files;
			TagService = tagService;
			ContextTools = contextTools;
			LoggedUser = accountProvider.GetLoggedUser();
		}

		public async Task SaveFromFilesAsync(IFormFileCollection files)
		{
			var todayDate = ContextTools.Today();
			foreach (var file in files)
				await SaveFromFileAsync(todayDate, file);
		}

		async Task SaveFromFileAsync(DateTime todayDate, IFormFile file)
		{
			var fileName = ContextTools.GetFileName(file.FileName);
			var hashedFileName = (fileName + ContextTools.Now().ToString()).Hash();
			var extension = ContextTools.GetFileExtension(file.ContentType);
			var piece = new Piece().Fill(fileName, hashedFileName, extension, LoggedUser.HashedFilePath, todayDate);
			await SaveAsync(piece);
			Files.Save(new HashableFile(file, hashedFileName, LoggedUser.HashedFilePath, extension));
		}

		public async Task RemoveAsync(IEnumerable<long> ids)
		{
			var pieces = await ByIdsAsync(ids);
			pieces.Select(p => p.AssertIsOwnedBy(LoggedUser));
			var piecesToRemoveTasks = new List<Task>();
			foreach(var piece in pieces)
			{
				Files.Remove(ContextTools.DefaultBasePath(), piece.HashedFilePath, piece.HashedFileName, piece.Extension);
				piecesToRemoveTasks.Add(Repository.RemoveAsync(piece));
			}
			await Task.WhenAll(piecesToRemoveTasks.ToArray());
		}

		public async Task UpdateAsync(PieceModel pieceModel)
		{
			var piece = await ByIdAsync(pieceModel.Id.Value);

			piece.AssertIsNotNull(pieceModel.Id.Value)
				 .AssertIsOwnedBy(LoggedUser);

			var savedTags = await TagService.SaveUniqueAsync(pieceModel.TagNames);
			pieceModel.FillTags(savedTags);
			await UpdateAsync(pieceModel.Update(piece));
		}

		public async Task UpdateAsync(IEnumerable<PieceModel> pieceModels)
		{
			//TODO: VALIDAR SE HÁ IDS REPETIDOS
			var pieces = await ByIdsAsync(pieceModels.Select(m => m.Id.Value));
			var piecesDictionary = new Dictionary<long, Piece>();
			foreach (var piece in pieces)
				if (piece.AssertIsOwnedBy(LoggedUser))
					piecesDictionary[piece.Id.Value] = piece;

			var savedTags = await TagService.SaveUniqueAsync(pieceModels.SelectMany(p => p.TagNames));
			foreach (var pieceModel in pieceModels)
				await UpdateAsync(pieceModel.FillTags(savedTags).Update(piecesDictionary[pieceModel.Id.Value]));
		}
	}
}
