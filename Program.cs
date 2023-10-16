using System;
using System.Collections.Generic;
using System.Linq;



class Note
{
    public string n { get; set; }
    public string d { get; set; }
    public DateTime Date { get; set; }

    public Notes(string n, string description, DateTime date)
    {
        n = n;
        d = description;
        Date = date;
    }
}
class Program
{
    static List<Notes> notes = new List<Notes>
    {
        new Note("Заметка 5", "Описание заметки 5", new DateTime(2023,10,15)),
        new Note("Заметка 4", "Описание заметки 4", new DateTime(2023,10,18)),
        new Note("Заметка 3", "Описание заметки 3", new DateTime(2023,10,13)),
        new Note("Заметка 2", "Описание заметки 2", new DateTime(2023,10,10)),
        new Note("Заметка 1", "Описание заметки 1", new DateTime(2023,10,8)),
    };

    static DateTime selectedDate = DateTime.Today;
    static List<Notes> notesForSelectedDate;
    static int selectedIndex = 0;
    static void UpdateNotesForSelectedDate()
    {
        notesForSelectedDate = notes.Where(n => n.Date.Date == selectedDate.Date).ToList();
        selectedIndex = 0;
    }

    static void AddNote()
    {
        Console.Clear();
        Console.WriteLine("Добавление новой заметки\n");
        Console.WriteLine("Введите заголовок заметки:");
        string title = Console.ReadLine();

        Console.WriteLine("Введите пояснение заметки:");
        string description = Console.ReadLine();

        Note newNote = new Note(title, description, selectedDate);
        notes.Add(newNote);

        Console.WriteLine("\nЗаметка успешно добавлена!");
        Console.WriteLine("\nДля продолжения нажмите любую клавишу.");

        Console.ReadKey();
        ShowMenu();
    }

    static void Main(string[] args)
    {
        Console.WriteLine("Ежедневник");

        ShowMenu();

        while (true)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);

            switch (key.Key)
            {
                case ConsoleKey.F1:
                    AddNote();
                    break;
                case ConsoleKey.LeftArrow:
                    DecrementSelectedDate();
                    ShowMenu();
                    break;
                case ConsoleKey.RightArrow:
                    IncrementSelectedDate();
                    ShowMenu();
                    break;
                case ConsoleKey.UpArrow:
                    DecrementSelectedIndex();
                    ShowMenu();
                    break;
                case ConsoleKey.DownArrow:
                    IncrementSelectedIndex();
                    ShowMenu();
                    break;
                case ConsoleKey.Escape:
                    Environment.Exit(0);
                    break;
                case ConsoleKey.Enter:
                    ShowNoteDetails();
                    break;
            }
        }
    }

    static void ShowMenu()
    {
        Console.Clear();
        Console.WriteLine("Ежедневник - " + selectedDate.ToShortDateString());
        Console.WriteLine(" Для добавлния новой заметки нажмите F1");
        Console.WriteLine("-----------\n");


        notesForSelectedDate = notes.Where(n => n.Date.Date == selectedDate.Date).ToList();

        if (notesForSelectedDate.Count > 0)
        {
            Console.WriteLine("Заметки:\n");
            

            for (int i = 0; i < notesForSelectedDate.Count; i++)
            {
                Note note = notesForSelectedDate[i];
                string arrow = i == selectedIndex ? "->" : " ";
                Console.WriteLine(arrow + " " + (i + 1) + ". " + note.n);
            }
        }
        

      
    }

    static void ShowNoteDetails()
    {
        Console.Clear();
        Console.WriteLine("Пояснение заметки (" + selectedDate.ToShortDateString() + "):\n");

        if (notesForSelectedDate.Count > 0)
        {
            Note selectedNote = notesForSelectedDate[selectedIndex];
            Console.WriteLine("Заголовок: " + selectedNote.n);
            Console.WriteLine("Пояснение: " + selectedNote.d);
            
        }
       

        Console.WriteLine("\nДля возврата нажмите любую клавишу.");

        Console.ReadKey();
        ShowMenu();
    }

    static void IncrementSelectedDate()
    {
        selectedDate = selectedDate.AddDays(1);
        UpdateNotesForSelectedDate();
    }

    static void DecrementSelectedDate()
    {
        selectedDate = selectedDate.AddDays(-1);
        UpdateNotesForSelectedDate();
    }

    static void IncrementSelectedIndex()
    {
        selectedIndex++;
        if (selectedIndex >= notesForSelectedDate.Count)
        {
            selectedIndex = 0;
        }
    }

    static void DecrementSelectedIndex()
    {
        selectedIndex--;
        if (selectedIndex < 0)
        {
            selectedIndex = notesForSelectedDate.Count - 1;
        }
    }

   

    
}

