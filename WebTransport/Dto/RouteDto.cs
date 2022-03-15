namespace WebTransport.Dto
{
    public class RouteDto
    {
        public string Number { get; set; }
        public string Type { get; set; }
        public RouteDto(string number, string type)
        {
            Number = number;
            Type = type;
        }
    }
}
