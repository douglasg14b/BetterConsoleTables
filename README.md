[![Nuget](https://img.shields.io/nuget/v/BetterConsoleTables.svg?style=flat-square)](https://www.nuget.org/packages/BetterConsoleTables)
[![Nuget](https://img.shields.io/nuget/vpre/BetterConsoleTables?style=flat-square)](https://www.nuget.org/packages/BetterConsoleTables/2.0.2-rc1)

**Note:** Readme is still WIP

# Better Consoles

Faster, colorable, more configurable, and more robust console colors & tables for C# console applications.

* Better Console Colors
* Better Console Tables



# Better Console Tables



## What it does

Provides tables for your console application! But really, it provides tables in a performance friendly way, while also adding the ability to display multiple tables in a variety of formats. There is additional configuration information that you can use to overwrite default functionality, allowing you to create tables with whatever style you want.

## Why?

To make something better than the defacto [console tables library](https://github.com/khalidabuhakmeh/ConsoleTables).

## How do I use it?

1. Get it from nuget `Install-Package BetterConsoleTables -Version 1.1.0`
2. Include it `using BetterConsoleTables;`
3. See code examples or example directory

## You mentioned performance?

Yes! Yes I did. I wrote this to not just be highly configurable, but also performance friendly. Version 2 adds significant complexity by allowing for coloring, formatting, and greater configuration flexibility. This, unfortunately, comes at a cost.

As you can see from the tests below, Version 2 takes ~1.1x as long as the defacto library. Version 1 is nearly 3x faster.

| Test                      | Mean     | StdDev  | Ratio |
|---------------------------|----------|---------|-------|
| OtherConsoleTable         | 9.8 us   | 0.86 us | 1.00  |
| v1                        | 3.78 us  | 0.15 us | 0.39  |
| v2                        | 10.89 us | 0.13 us | 1.11  |
| v2 Formatted              | 14.96 us | 0.29 us | 1.52  |
| v2 Formatted Replace Rows | 13.97 us | 0.13 us | 1.42  |

## Features

* Every table cell can have it's own formatting
   * Current API is inelegant, better API being developed `[Coming Soon]`
* Formatting Callbacks/Custom Formatters
* Configuration Flexibility
   * Fluent API
   * Config classes
   * Pre-formatted strings
* Print multiple tables
  * Automatically lines up the columns and their widths between the tables
* Configurable table box drawing formats
  * Several presets to choose from
  * Can change any of the table drawing characters in the configuration
  * Dividers, headers, outside & inside edges, and even corners are configurable
* Alignment
  * Align headers and cells, or entire columns
  * Left, Center, and Right alignment with automatic padding
* Colors
  * Full RGB support
  * Background & Foreground coloring
  * Gradients `[Coming Soon]`
  * Value-based coloring `[Coming Soon]`
* Font/Text Formatting
  * Bold (Brighten)
  * Underline
  * Italic <sup>**</sup>
  * Blinking
  * Crossed Out <sup>**</sup>
  * Overline <sup>**</sup>
  * Reversed colors (Swaps foreground & background)
* Table data replacement support
* Generate tables from existing objects

## Future Improvements

* Configuration
   * Easier, short-form, configuration so there isn't as much boilerplate to write

<sup>**</sup> Does not work in default windows console

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

#### Derive From Objects

```cs
static void Main(String[] args)
{
  Table table = new Table(TableConfig.MySql());
  table.From<SomeData>(rows);

  Console.Write(table.ToString());
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

#### Column Alignment

```cs
    ColumnHeader[] headers = new[]
    {
        new ColumnHeader("Left"),
        new ColumnHeader("Left Header", Alignment.Right),
        new ColumnHeader("Right Header", Alignment.Center, Alignment.Right),
    };
    Table table = new Table(headers);
    
    table.Config = TableConfiguration.MySqlSimple();
    table.AddRow("1", "2", "3");
    table.AddRow("Short", "item", "Here");
    table.AddRow("Longer items go here", "Right Contents", "Centered Contents");

    Console.Write(table.ToString());
    Console.ReadKey();
 ```

## Console Outputs

#### Column & Row Alignment 1

```cs
ColumnHeader[] headers = new[]
{
    new ColumnHeader("Left"),
    new ColumnHeader("Right", Alignment.Right, Alignment.Right),
    new ColumnHeader("Center", Alignment.Center, Alignment.Center),
};

Table table = new Table(headers);
```


```
+----------------------+-------------+---------------------+
| Left                 |       Right |        Center       |
+----------------------+-------------+---------------------+
| 1                    |           2 |          3          |
| Short                |        item |         Here        |
| Longer items go here | stuff stuff | some centered thing |
+----------------------+-------------+---------------------+
```

#### Column & Row Alignment 2

```cs
ColumnHeader[] headers = new[]
{
    new ColumnHeader("Left"),
    new ColumnHeader("Left Header", Alignment.Right),
    new ColumnHeader("Right Header", Alignment.Center, Alignment.Right),
};

Table table = new Table(headers);
```

```
+----------------------+----------------+-------------------+
| Left                 | Left Header    |      Right Header |
+----------------------+----------------+-------------------+
| 1                    |              2 |         3         |
| Short                |           item |        Here       |
| Longer items go here | Right Contents | Centered Contents |
+----------------------+----------------+-------------------+
```

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

