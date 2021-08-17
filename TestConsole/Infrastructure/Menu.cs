using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace TestConsole.Infrastructure
{
    public class Menu : IEnumerable<MenuEntry>
    {
        private readonly Collection<MenuEntry> entries = new Collection<MenuEntry>();

        public void Add(string name, string description, Action action)
        {
            Add(name, description, () =>
            {
                action.Invoke();
                return Task.CompletedTask;
            });
        }

        public void Add(string name, string description, Func<Task> action)
            => Add(new MenuEntry { Name = name, Description = description, Action = action });

        public void Add(MenuEntry menuEntry)
            => entries.Add(menuEntry);

        public IEnumerator<MenuEntry> GetEnumerator()
        {
            throw new NotSupportedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void PrintEntries(IEnumerable<MenuEntry> set)
        {
            var len = set.Max(e => e.Name.Length);
            var oldColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{"Name".PadRight(len)}\tDescription");
            Console.ForegroundColor = oldColor;

            foreach (var entry in set)
            {
                Console.WriteLine($"{entry.Name.PadRight(len)}\t{entry.Description}");
            }
            Console.WriteLine();
        }

        public async Task RunAsync()
        {
            PrintEntries(entries);

            Func<Task> action = null;
            do
            {
                Console.Write("Enter menu entry name: ");
                var response = Console.ReadLine();
                if (response == null) return;

                if (string.IsNullOrWhiteSpace(response)) continue;

                var matches = entries.Where(e => e.Name.StartsWith(response, StringComparison.InvariantCultureIgnoreCase)).ToList();
                if (matches.Count == 0)
                {
                    Console.WriteLine("No matching entry found");
                    continue;
                }

                if (matches.Count == 1)
                {
                    action = matches[0].Action;
                }
                else
                {
                    var exact = matches.SingleOrDefault(m => m.Name.Equals(response, StringComparison.InvariantCultureIgnoreCase));
                    if (exact != null)
                    {
                        action = exact.Action;
                    }
                    else
                    {
                        Console.WriteLine($"Multiple entries matching {response}:");
                        PrintEntries(matches);
                    }
                }
            } while (action == null);

            await RunActionAsync(action);
        }

        private static async Task RunActionAsync(Func<Task> action)
        {
            try
            {
                await action();
            }
            catch (Exception e)
            {
                Console.WriteLine(@"Error occured during action invocation:");
                Console.WriteLine();
                Console.WriteLine(e.Message);
                Console.WriteLine();

                var confirmed = false;
                while (!confirmed)
                {
                    Console.Write(@"Print stack trace? (y/n) ");
                    var r = char.ToLower(Console.ReadKey().KeyChar);

                    Console.WriteLine();

                    switch (r)
                    {
                        case 'y':
                            Console.WriteLine(e);
                            confirmed = true;
                            break;
                        case 'n':
                            confirmed = true;
                            break;
                        default:
                            Console.WriteLine(@"Please respond with y or n");
                            break;
                    }
                }
            }
        }
    }
}