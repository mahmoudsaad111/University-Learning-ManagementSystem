namespace Application.Common.Interfaces.Presistance
{
    public interface ICRUDAsync<T> where T : class
    {
        public Task<T> CreateAsync(T entity);
        public Task<bool> UpdateAsync(T? entity);
        public Task<T> GetByIdAsync(int? Id);
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<bool> DeleteAsync(int? Id);

    }
}
