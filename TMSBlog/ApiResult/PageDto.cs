﻿using Newtonsoft.Json;

namespace TMSBlog.ApiResult
{
    public class PageDto<T>
    {
        /// <summary>
        /// Коллекция найденных экземпляров
        /// </summary>
        [JsonProperty("items")]
        public T[] Items { get; set; }

        /// <summary>
        /// Полное количество экземпляров в хранилище
        /// </summary>
        [JsonProperty("totalCount")]
        public int TotalCount { get; set; }
    }
}
