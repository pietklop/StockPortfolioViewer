using System;
using System.Threading.Tasks;

namespace TestConsole.Infrastructure
{
    public class MenuEntry
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Func<Task> Action { get; set; }
    }
}