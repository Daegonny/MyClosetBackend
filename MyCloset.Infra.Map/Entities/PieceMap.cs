using MyCloset.Domain.Entities;
using MyCloset.Infra.Map.Seed;

namespace MyCloset.Infra.Map.Entities
{
	public class PieceMap : EntityMap<Piece>
	{
		public PieceMap()
		{
			Table("piece");
			Map(x => x.FileName).Column("file_name");
			Map(x => x.Extension);
			Map(x => x.HashedFileName).Column("hashed_file_name");
			Map(x => x.HashedFilePath).Column("hashed_file_path");
			Map(x => x.Price);
			Map(x => x.Purchase);

			HasManyToMany(x => x.Tags).Table("piece_tag");

		}
	}
}
