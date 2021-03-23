namespace Base.Domain
{
	public abstract class EntityModel<T>
		where T : Entity
	{
		public virtual long? Id { get; set; }
		public virtual string Name { get; set; }
		public EntityModel() {}

		public EntityModel(long id, string name)
		{
			Id = id;
			Name = name;
		}

		public abstract T Update(T entity);
	}
}
