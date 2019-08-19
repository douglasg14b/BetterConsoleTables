[![Nuget](https://img.shields.io/nuget/v/BetterConsoleTables.svg?style=flat-square)](https://www.nuget.org/packages/BetterConsoleTables)

# BetterConsoleTables

Faster, more configurable, and more robust console tables for C# console applications

## What it does

Provides tables for your console application! But really, it provides tables in a very performance friendly way, while also adding the ability to display multiple tables in a variety of formats. There is additional configuration information that you can use to overwrite default functionality, allowing you to create tables with whatever style you want.

## Why?

To make something better than the defacto [console tables library](https://github.com/khalidabuhakmeh/ConsoleTables).

## How do I use it?

1. Get it from nuget `Install-Package BetterConsoleTables -Version 1.0.3`
2. Include it `using BetterConsoleTables;`
3. See code examples or example directory

## You mentioned performance?

Yes! Yes I did. I wrote this to not just be configurable, but also performance friendly. I measure it's performance in ticks (10,000 ticks/millisecond).

Generating a default table in the same manner as seen in the first example takes approximately 1900 ticks (19,000 nanoseconds), or 0.19 milliseconds. I'm happy with this number, considering it's an order of magnitude faster than other libraries.


## Code Examples

#### Single Simple Table

```cs
static void Main(String[] args)
{
    Table  table = new Table("one", "two", "three");
    table.AddRow(1, 2, 3)
         .AddRow("long line goes here", "short text", "word");

    Console.Write(table.ToString());
    Console.ReadKey();
}
```

#### Multiple Tables


```cs
static void Main(String[] args)
{
    Table table = new Table("One", "Two", "Three")
      .AddRow("1", "2", "3")
      .AddRow("Short", "item", "Here")
      .AddRow("Longer items go here", "stuff stuff", "stuff");

    Table table2 = new Table("One", "Two", "Three", "Four")
      .AddRow("One", "Two", "Three")
      .AddRow("Short", "item", "Here", "A fourth column!!!")
      .AddRow("stuff", "longer stuff", "even longer stuff in this cell")
      .Config = Config.UnicodeAlt();

    ConsoleTables tables = new ConsoleTables(table, table2);
    
    Console.Write(tables.ToString());
    
    Console.ReadKey();
}
```

## Console Outputs

#### Default

```
----------------------------------------------
| One                  | Two         | Three |
----------------------------------------------
| 1                    | 2           | 3     |
----------------------------------------------
| Short                | item        | Here  |
----------------------------------------------
| Longer items go here | stuff stuff | stuff |
----------------------------------------------
```
#### Markdown
```cs
table.Config = TableConfiguration.Markdown();
```

```
| One                  | Two         | Three |
|----------------------|-------------|-------|
| 1                    | 2           | 3     |
| Short                | item        | Here  |
| Longer items go here | stuff stuff | stuff |
```

#### MySql
```cs
table.Config = TableConfiguration.MySql();
```
```
+----------------------+-------------+-------+
| One                  | Two         | Three |
+----------------------+-------------+-------+
| 1                    | 2           | 3     |
+----------------------+-------------+-------+
| Short                | item        | Here  |
+----------------------+-------------+-------+
| Longer items go here | stuff stuff | stuff |
+----------------------+-------------+-------+
```

#### MySql Simple
```cs
table.Config = TableConfiguration.MySqlSimple();
```
```
+----------------------+-------------+-------+
| One                  | Two         | Three |
+----------------------+-------------+-------+
| 1                    | 2           | 3     |
| Short                | item        | Here  |
| Longer items go here | stuff stuff | stuff |
+----------------------+-------------+-------+
```

#### Unicode
```cs
table.Config = TableConfiguration.Unicode();
```
```
┌──────────────────────┬─────────────┬───────┐
│ One                  │ Two         │ Three │
├──────────────────────┼─────────────┼───────┤
│ 1                    │ 2           │ 3     │
│ Short                │ item        │ Here  │
│ Longer items go here │ stuff stuff │ stuff │
└──────────────────────┴─────────────┴───────┘
```
#### Unicode Alt
```cs
 table.Config = TableConfiguration.UnicodeAlt();
 ```
```
╔══════════════════════╦═════════════╦═══════╗
║ One                  ║ Two         ║ Three ║
╠══════════════════════╬═════════════╬═══════╣
║ 1                    ║ 2           ║ 3     ║
║ Short                ║ item        ║ Here  ║
║ Longer items go here ║ stuff stuff ║ stuff ║
╚══════════════════════╩═════════════╩═══════╝
```

## Screenshot

![alt text](https://raw.githubusercontent.com/douglasg14b/BetterConsoleTables/master/Screenshot_6.png)

