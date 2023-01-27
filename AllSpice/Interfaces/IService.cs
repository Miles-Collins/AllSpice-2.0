namespace AllSpice.Interfaces
{
  public interface IService<T, Tid>
  {
    List<T> Get();

    T GetById(Tid id);

    T Create(T data);

    T Update(T data, string id);

    string Delete(Tid id, string userId);
  }
}