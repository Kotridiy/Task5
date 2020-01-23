using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface IGeneralService<TModelDTO> where TModelDTO : class
    {
        void Add(TModelDTO itemDTO);
        void Delete(int id);
        void Edit(int id, TModelDTO item);
        TModelDTO Get(int id);
        IEnumerable<TModelDTO> GetAll();
    }
}