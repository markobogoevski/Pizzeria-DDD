namespace Contracts.Filters
{
    using Microsoft.AspNetCore.Mvc;

    public class FilterBySkipTakeRequest
    {
        public FilterBySkipTakeRequest()
        {
            Skip = uint.MinValue;
            Take = ushort.MaxValue;
        }

        [FromQuery(Name = "skip")]
        public uint Skip { get; set; }

        [FromQuery(Name = "take")]
        public ushort Take { get; set; }
    }
}
