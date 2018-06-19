namespace Eagles.Application.Model.AppModel.Module
{
    public class Module
    {
        /// <summary>
        /// ModuleId
        /// </summary>
        public int ModuleId { get; set; }

        /// <summary>
        /// ModuleName
        /// </summary>
        public string ModuleName { get; set; }

        /// <summary>
        /// TragetUrl
        /// </summary>
        public string TragetUrl { get; set; }

        /// <summary>
        /// IndexPageCount
        /// </summary>
        public int IndexPageCount { get; set; }

        /// <summary>
        /// IndexDisplay
        /// </summary>
        public bool IndexDisplay { get; set; }

        /// <summary>
        /// SmallImageUrl
        /// </summary>
        public string SmallImageUrl { get; set; }

        /// <summary>
        /// ImageUrl
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Priority
        /// </summary>
        public int Priority { get; set; }

        //public List<NewsInfo> News { get; set; }

    }
}
