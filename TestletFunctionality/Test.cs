namespace TestletFunctionality
{
    public class Test
    {
        public string Id { get; set; }
        public TestTypeEnum Type { get; set; }
        public Test(string id, TestTypeEnum type)
        {
            Id = id;
            Type = type;
        }
    }
}