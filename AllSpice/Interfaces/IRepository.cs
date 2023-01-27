namespace AllSpice.Interfaces
{
  public interface IRepository<T, Tid>
  {
    List<T> Get();

    T GetById(Tid id);

    T Create(T data);

    T Update(T newData);

    void Delete(Tid id);
  }
}