namespace Store.Entities
{
    public class GoodCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ParentID { get; set; }
        public virtual GoodCategory ParentCategory { get; set; }
        public virtual ICollection<GoodCategory> ChildCategories { get; set; }

    }
}