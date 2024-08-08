namespace RSystem.Model.ResponseModel
{
   public class ClientResponseListModel<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public T data { get; set; }
       
    }
}
