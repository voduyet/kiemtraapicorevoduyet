using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static kiemtraVoDuyet.Model.ContextDB;

namespace kiemtraVoDuyet.Model
{
    public class ContextDB : DbContext
    {

        public string ConnectionString { get; private set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Product_Image> Product_Images { get; set; }

        public ContextDB()
        {
            var folder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var pathApp = Path.Combine(folder, "HelloEFCore");
            if (!Directory.Exists(pathApp))
            {
                Directory.CreateDirectory(pathApp);
            }
            ConnectionString = Path.Combine(pathApp, "kiemtra.db");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source = {ConnectionString}");
        }
    }
    public class BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
    public class Category : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public virtual List<Product> Products { get; set; }
    }
    public class Product : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public string Code { get; set; }
        public double Price { get; set; }
        public bool? Continue { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public virtual Category Category { get; set; }
        public virtual List<Product_Image> Product_Images { get; set; }

    }
    public class Product_Image : BaseEntity
    {
        public int IdProduct { get; set; }
        public int IdImage { get; set; }

        [ForeignKey("IdProduct")]
        public virtual Product Product { get; set; }
        [ForeignKey("IdImage")]
        public virtual Image Image { get; set; }

    }
    public class Image : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string FileName { get; set; }
        public virtual List<Product_Image> Product_Images { get; set; }


    }
    public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }


}
