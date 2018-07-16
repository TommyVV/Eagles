namespace Eagles.Application.Model.Organization.Requset
{
     /// <summary>
     /// 
     /// </summary>
     public class GetOrganizationRequset : PageRequestBase
    {
        /// <summary>
        /// 省id
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// 市id
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 区id
        /// </summary>
        public string District { get; set; }
    }
}
