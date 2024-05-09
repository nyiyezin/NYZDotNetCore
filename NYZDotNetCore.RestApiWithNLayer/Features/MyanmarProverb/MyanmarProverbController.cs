using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace NYZDotNetCore.RestApiWithNLayer.Features.MyanmarProverb
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyanmarProverbController : ControllerBase
    {
        private async Task<MyanmarProverb> GetDataAsync()
        {
            string jsonStr = await System.IO.File.ReadAllTextAsync("MyanmarProverbs.json");
            var model = JsonConvert.DeserializeObject<MyanmarProverb>(jsonStr);
            return model;
        }

        [HttpGet]
        public async Task<IActionResult> AlphabetList()
        {
            var model = await GetDataAsync();
            return Ok(model.Tbl_MMProverbsTitle.ToList());
        }

        [HttpGet("{titleId}/Proverbs")]
        public async Task<IActionResult> ProverbList(int titleId)
        {
            var model = await GetDataAsync();
            return Ok(model.Tbl_MMProverbs.Where(proverb => proverb.TitleId == titleId).ToList());
        }

        [HttpGet("{titleId}/Proverbs/{proverbId}")]
        public async Task<IActionResult> ProverbsDesc(int titleId, int proverbId)
        {
            var model = await GetDataAsync();
            return Ok(model.Tbl_MMProverbs.FirstOrDefault(x => x.TitleId == titleId && x.ProverbId == proverbId));
        }
    }


    public class MyanmarProverb
    {
        public Tbl_Mmproverbstitle[] Tbl_MMProverbsTitle { get; set; }
        public Tbl_Mmproverbs[] Tbl_MMProverbs { get; set; }
    }

    public class Tbl_Mmproverbstitle
    {
        public int TitleId { get; set; }
        public string TitleName { get; set; }
    }

    public class Tbl_Mmproverbs
    {
        public int TitleId { get; set; }
        public int ProverbId { get; set; }
        public string ProverbName { get; set; }
        public string ProverbDesp { get; set; }
    }

}
