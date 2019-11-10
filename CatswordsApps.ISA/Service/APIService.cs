namespace CatswordsApps.ISA.Service
{
    public static class APIService
    {
        public static string GetURL(string apiName)
        {
            return GetURL() + "/?route=" + apiName;
        }

        public static string GetURL()
        {
            return "https://catswords.re.kr/ep/";
        }
    }
}
