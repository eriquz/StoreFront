using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;//Added for metadata and validation

namespace StoreFront.DATA.EF //.StoreFrontMetadata Comment this Out to allow function matching the original classes to allow partical view to function
{
    class StoreFrontMetadata
    {
        #region productMetadata
        public class ProductMetadata
        {
            //public int ProductID { get; set; } Don't want the user to see this.

            [Required(ErrorMessage = "*Product Name is Required")]
            [StringLength(100, ErrorMessage = "*Must be 100 characters or less")]
            [Display(Name = "Product")]
            public string ProductName { get; set; }

            [Required(ErrorMessage = "*Shoe Size is required")]
            [Display(Name = "Shoe Size")]
            public int ShoeSizeID { get; set; }

            [Required(ErrorMessage = "*Product Manufacture is Required")]
            [Display(Name = "Product Manufacture")]
            public int ManufactureID { get; set; }

            [Required(ErrorMessage = "*Category is Required")]
            [Display(Name = "Category")]
            public int CategoryID { get; set; }

            [Required(ErrorMessage = "*Price is Required")]
            [Range(0, double.MaxValue, ErrorMessage = "*Must be a valid number 0 or larger.")]
            [DisplayFormat(DataFormatString = "{0:c}")]
            public decimal Price { get; set; }

            [Display(Name = "Units InStock")]
            public Nullable<int> UnitsInStock { get; set; }

            [Display(Name = "The Status")]
            public Nullable<int> StatusID { get; set; }

            [DisplayFormat(NullDisplayText = "N/A")]
            [Display(Name = "Product Description")]
            [UIHint("MultilineText")]
            public string Description { get; set; }

            [Display(Name = "Image")]
            public string ProductImage { get; set; }
        }

        [MetadataType(typeof(ProductMetadata))]
        public partial class Product { }

        #endregion

        #region CategoryMetadata

        public class CategoryMetadata
        {
            //public int CategoryID { get; set; }

            [Required(ErrorMessage = "*Needs Mens,Women, Girls or Boys")]
            [StringLength(50, ErrorMessage = "*Must be 50 characters or less")]
            [Display(Name = "Category")]
            public string CategoryName { get; set; }
        }

        [MetadataType(typeof(CategoryMetadata))]
        public partial class Category { }
        #endregion

        #region StatusMetadata

        public class StatusMetadata
        {
            //public int StatusID { get; set; }

            [Required(ErrorMessage = "*")]
            [StringLength(25, ErrorMessage = "*Must be 25 characters or less")]
            [Display(Name = "Product Status")]
            public string Status1 { get; set; }
        }

        [MetadataType(typeof(StatusMetadata))]
        public partial class Status { }

        #endregion

        #region ShoeSizeMetadata

        public class ShoeSizeMetadata
        {

            public int ShoeSizeID { get; set; }
            [Required(ErrorMessage = "* Size is required")]
            [Display(Name = "Size")]
            public int Size { get; set; }
        }
        [MetadataType(typeof(ShoeSizeMetadata))]
        public partial class ShoeSize { }
        #endregion

        #region ManufacturerMetadata

        public class ManufacturerMetadata
        {
            [Required(ErrorMessage = "* Supplier Must Be Listed")]
            [Display(Name = "Supplier")]
            public int ManufactureID { get; set; }

            [Required(ErrorMessage = "* Supplier Must Be Listed")]
            [Display(Name = "Supplier Name")]
            [StringLength(50, ErrorMessage = "* Must not exceed 50 characters")]
            public string ManufactureName { get; set; }

            [Required(ErrorMessage = "*Country Required")]
            [Display(Name = "City")]
            [StringLength(60, ErrorMessage = "* Must not exceed 50 characters")]
            public string Country { get; set; }

            [Required(ErrorMessage = "*City Required")]
            [Display(Name = "City")]
            [StringLength(60, ErrorMessage = "*Must not exceed 50 characters")]
            public string City { get; set; }

            [Required(ErrorMessage = "*State Required")]
            [Display(Name = "State")]
            [StringLength(2, ErrorMessage = "*Must not exceed 2 characters")]
            public string State { get; set; }

        }
       [MetadataType(typeof(ManufacturerMetadata))]
        public partial class Manufacturer { }
        #endregion

        #region EmployeeMetaData

        public class EmployeeMetadata
        {
            //public int EmployeeID { get; set; }

            [Required(ErrorMessage = "*")]
            [StringLength(25, ErrorMessage = "*Must be 25 characters or less")]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Required(ErrorMessage = "*")]
            [StringLength(25, ErrorMessage = "*Must be 25 characters or less")]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [DisplayFormat(NullDisplayText = "N/A")]
            [Range(1, int.MaxValue, ErrorMessage = "Must be a valid number 0 or larger.")]
            public Nullable<int> DirectReportID { get; set; }


            //TODO: RegEx for Phone Number?
            [Required(ErrorMessage = "*")]
            [Range(10, int.MaxValue, ErrorMessage = "Must be a valid number 0 or larger.")]
            [Display(Name = "Phone Number")]
            public int PhonePhoneNumber { get; set; }

            [Required(ErrorMessage = "*")]
            [Range(1, int.MaxValue, ErrorMessage = "Must be a valid number 0 or larger.")]
            public int DepartmentID { get; set; }
        }
        [MetadataType(typeof(EmployeeMetadata))]
        public partial class Employee { }
        #endregion

        #region DepartmentMetadata
        public class DepartmentMetadata
        {
            [Display(Name = "Department Name")]
            [Required(ErrorMessage = "*")]
            [StringLength(50, ErrorMessage = "* Value must be 50 characters or less.")]
            public string DepartmentName { get; set; }
        }
        [MetadataType(typeof(DepartmentMetadata))]
        public partial class Department { }
        #endregion
    }
}
