using kiemtraVoDuyet.Model;

namespace kiemtraVoDuyet.Service
{
    public class CategoryService
    {
        private readonly ContextDB context;
        public CategoryService(ContextDB context)
        {
            this.context = context;
        }
        public List<Category> GetAll()
        {
            return context.Categories.ToList();
        }
        public bool Add(Category category)
        {
            var exist = context.Categories.Where(e => e.Id == category.Id || e.Name == category.Name).FirstOrDefault();
            if (exist == null)
            {
                context.Add(category);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Update(Category category)
        {
            var obj = context.Categories.FirstOrDefault(e => e.Id == category.Id);
            if (obj == null)
            {
                return false;
            }
            else
            {
                obj.Name = category.Name;
                context.SaveChanges();
                return true;
            }
        }
        public bool Delete(int id)
        {
            var obj = context.Categories.FirstOrDefault(e => e.Id == id);
            if (obj != null)
            {
                context.Remove(obj);
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
