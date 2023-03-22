using kiemtraVoDuyet.Model;


namespace kiemtraVoDuyet.Service
{
    public class ProductService
    {
        private readonly ContextDB context;
        public ProductService(ContextDB db)
        {
            context = db;
        }

        public List<Product> GetAll()
        {
            return context.Products.ToList();
        }
        public bool Add(Product product)
        {
            var exist = context.Products.Where(e => e.Id == product.Id || e.Name == product.Name).FirstOrDefault();
            if (exist == null)
            {
                context.Add(product);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Update(Product product)
        {
            var obj = context.Products.FirstOrDefault(e => e.Id == product.Id);
            if (obj == null)
            {
                return false;
            }
            else
            {
                obj.Name = product.Name;
                obj.Price = product.Price;
                obj.Code = product.Code;
                obj.Continue = product.Continue;
                obj.DateUpdated = DateTime.Now;
                obj.CategoryId = product.CategoryId;
                context.SaveChanges();
                return true;
            }
        }
        public bool Delete(int id)
        {
            var obj = context.Products.FirstOrDefault(e => e.Id == id);
            if (obj == null)
            {
                context.Remove(id);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}