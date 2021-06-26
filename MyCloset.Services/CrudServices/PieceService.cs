using Exceptions;
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
	public class PieceService : CrudService<Piece, IPieceQueryFilter, PieceModel>, IPieceService
	{
		IPieces Pieces { get; }
		IFiles Files { get; }
		ITagService TagService { get; }
		IContextTools ContextTools { get; }

		public PieceService
		(
			IPieces pieces, 
			IFiles files,
			ITagService tagService,
			IContextTools contextTools
		) : base(pieces, contextTools)
		{
			Pieces = pieces;
			Files = files;
			TagService = tagService;
			ContextTools = contextTools;
		}

		public async Task SaveFromFilesAsync(IFormFileCollection files, string hashedUserPath)
		{
			var todayDate = ContextTools.Today();
			foreach (var file in files)
				await SaveFromFileAsync(hashedUserPath, todayDate, file);
		}

		async Task SaveFromFileAsync(string hashedUserPath, DateTime todayDate, IFormFile file)
		{
			var fileName = ContextTools.GetFileName(file.FileName);
			var hashedFileName = (fileName + ContextTools.Now().ToString()).Hash();
			var extension = ContextTools.GetFileExtension(file.ContentType);
			Files.Save(new HashableFile(file, hashedFileName, hashedUserPath, extension));
			var piece = new Piece().Fill(fileName, hashedFileName, extension, hashedUserPath, todayDate);
			await SaveAsync(piece);
		}

		public new async Task RemoveAsync(long id)
		{
			var piece = await ByIdAsync(id);
			Files.Remove(ContextTools.DefaultBasePath(), piece.HashedFilePath, piece.HashedFileName, piece.Extension);
			await base.RemoveAsync(piece);
		}

		public async Task RemoveAsync(IEnumerable<long> ids)
		{
			var pieces = await ByIdsAsync(ids);
			var piecesToRemoveTasks = new List<Task>();
			foreach(var piece in pieces)
			{
				Files.Remove(ContextTools.DefaultBasePath(), piece.HashedFilePath, piece.HashedFileName, piece.Extension);
				piecesToRemoveTasks.Add(RemoveAsync(piece));
			}
			await Task.WhenAll(piecesToRemoveTasks.ToArray());
		}

		public override async Task UpdateAsync(PieceModel pieceModel)
		{
			var savedTags = await TagService.SaveUniqueAsync(pieceModel.TagNames);
			pieceModel.FillTags(savedTags);
			await base.UpdateAsync(pieceModel);
		}

		public override async Task UpdateAsync(IEnumerable<PieceModel> pieceModels)
		{
			var savedTags = await TagService.SaveUniqueAsync(pieceModels.SelectMany(p => p.TagNames));
			foreach (var pieceModel in pieceModels)
				pieceModel.FillTags(savedTags);
			await base.UpdateAsync(pieceModels);
		}
	}
}
