
# TotalCommanderConsoleApp

TotalComman<img width="511" height="511" alt="image" src="https://github.com/user-attachments/assets/a452cbc5-5809-43ac-bbee-efc41afb8513" />
derConsoleApp/
├── Models/               # Data modellari
│   ├── FileSystemItem.cs
│   ├── FileOperationResult.cs
│   └── AppConfig.cs
├── Services/             # Biznes logikasi
│   ├── FileManager.cs
│   ├── DirectoryService.cs
│   └── CommandParser.cs
├── Views/                # UI bilan ishlash
│   ├── ConsoleView.cs
│   └── HelpView.cs
├── Commands/             # Buyruqlar uchun
│   ├── ICommand.cs
│   ├── CopyCommand.cs
│   ├── DeleteCommand.cs
│   └── ... (boshqa commandlar)
├── Utilities/            # Yordamchi funksiyalar
│   ├── FileSizeFormatter.cs
│   ├── PathHelper.cs
│   └── Logger.cs
├── Program.cs            # Asosiy kirish nuqtasi
└── App.config            # Konfiguratsiya fayli
