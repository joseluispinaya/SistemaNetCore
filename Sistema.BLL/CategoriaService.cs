using Sistema.DAL;
using Sistema.Entity;

namespace Sistema.BLL
{
    public class CategoriaService
    {
        private readonly CategoriaData _categoriaData;

        public CategoriaService(string connectionString)
        {
            _categoriaData = new CategoriaData(connectionString);
        }

        public List<Categoria> GetAllCategorias()
        {
            return _categoriaData.GetAllCategorias();
        }

        public Categoria GetCategoriaById(int IdCategoria)
        {
            return _categoriaData.GetCategoriaById(IdCategoria);
        }

        public bool AddCategoria(Categoria categoria)
        {
            return _categoriaData.AddCategoria(categoria);
        }

        public bool UpdateCategoria(Categoria categoria)
        {
            return _categoriaData.UpdateCategoria(categoria);
        }
    }
}
