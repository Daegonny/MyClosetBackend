using Infra.Abstractions;
using MyCloset.Domain.Entities;
using MyCloset.Domain.Models;
using MyCloset.Infra.Abstractions.QueryFilters;
using MyCloset.Infra.Abstractions.Repositories;
using MyCloset.Services.Abstractions.CrudServices;
using MyCloset.Services.Seed;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Util.Extensions;
using Util.Services;

namespace MyCloset.Services.CrudServices
{
	public class TagService : CrudService<Tag, ITagQueryFilter, TagModel>, ITagService
	{
		ITags Tags { get; }
		public TagService(ITags tags, IContextTools contextTools) : base(tags, contextTools)
		{
			Tags = tags;
		}

		public async Task<Dictionary<string, Tag>> SaveUniqueAsync(IEnumerable<string> tagNames)
		{
			var uniqueTagModelNames = tagNames.Select(t => t.Clean()).Distinct();
			var tagsOnBaseDictionary = await ByNamesAsync(uniqueTagModelNames);
			var tagsToSave = TagsNotOnBaseDictionary(uniqueTagModelNames, tagsOnBaseDictionary);
			var newSavedTags = (await SaveAsync(tagsToSave));
			return MergeTags(tagsOnBaseDictionary, newSavedTags);
		}

		private static List<Tag> TagsNotOnBaseDictionary(IEnumerable<string> tagModelNames, Dictionary<string, Tag> tagsOnBaseDictionary)
		{
			var tagsToSave = new List<Tag>();
			foreach (var tagModelName in tagModelNames)
				if (!tagsOnBaseDictionary.ContainsKey(tagModelName))
					tagsToSave.Add(new Tag(0, tagModelName));
			return tagsToSave;
		}

		//TODO: Mover para extensions
		private Dictionary<string, Tag> MergeTags(Dictionary<string, Tag> tagsOnBaseDictionary, IEnumerable<Tag> newSavedTags)
		{
			foreach (var newSavedTag in newSavedTags)
				if (!tagsOnBaseDictionary.ContainsKey(newSavedTag.Name))
					tagsOnBaseDictionary[newSavedTag.Name] = newSavedTag;
			return tagsOnBaseDictionary;
		}

		private async Task<Dictionary<string, Tag>> ByNamesAsync(IEnumerable<string> tagNames) 
			=> (await Tags.ByNamesAsync(tagNames)).ToDictionary(t => t.Name, t => t);
	}
}
