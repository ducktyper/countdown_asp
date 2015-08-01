## Coding practice
Goal: Learn C# and ASP.NET MVC

We will build a self-checkout machine used in a supermarket together.

### Task 2

#### TODO
Implement new requirements to store.rb and store_test.rb. Reading Ruby syntax will be useful to do this task.
#### Requirements
* Receipts show costs including cents (example: apple $5.00)

* Customers can purchase products which returns the receipt
```csharp
store.Purchase(new string[] {"0001", "0002"}) //=>
apple $5.00
orange $10.00
total $15.00
```
* Owner can view purchase summary as data array
```csharp
store.PurchaseSummary() //=>
[
  ["Time","Number of Products","Cost"],
  ["17/07/2015","2","20.00"],
  ["18/07/2015","1","15.99"]
]
```
* Owner can add discount to a product
```csharp
store.AddDiscount("0001", 1) //=> $1 discount to product "0001"
```
* Owner can delete discount to a product
```csharp
store.DeleteDiscount("0001") //=> delete discount to product "0001"
```

### Task 1

#### TODO
git checkout task1

Create Store.cs to Countdown project

Create StoreTest.cs to Countdown.Tests project

TDD to implement requirements

#### Requirements

User can add an item to store
```csharp
store.AddItem("0001", "apple", 10) // add $10 apple with barcode 0001
```
User can count stored items
```csharp
store.ItemCount() //=> 10 (10 items)
```
User can calculate total cost of given items
```csharp
store.CalculateCost(new string[] {"0001", "0001"}) //=> 20 (two apples costs $20)
```
User can print receipt of given items
```csharp
store.PrintReceipt(new string[] {"0001", "0001"}) //=>
apple $10
apple $10
total $20
```

### Setup environment
Install Visual Studio 2013

Here is basic ruby syntax useful to this task.

#### C# Syntax
##### Define Class
[More info](https://msdn.microsoft.com/en-nz/library/x9afc042.aspx)
###### class with no argument
```csharp
public class Item
{
    public Item()
    {
    }
}
```

###### create with 1 argument
```csharp
public class Item
{
    public Item(string name)
    {
    }
}
```

###### Create object
```csharp
Item item1 = new Item();
Item item2 = new Item("apple");
var item2 = new Item("apple"); // var represent any type
```

###### instance variable (start with @)
```csharp
public class Item
{
	private string name;
    public Item(string n)
    {
        name = n;
    }
    public print()
    {
    	Console.WriteLine(name);
    }
}
new Item("apple").print(); //=> "apple"
```

###### instance method
* declear method
```csharp
public class Item
{
    public int price(int quantity)
    {
        return 10 * quantity;
    }
}
new Item("apple").print(); //=> "apple"
```

* call method
```csharp
int price = new Item().price(5); //=> 50
```

###### Accessors
[More info](https://msdn.microsoft.com/en-us/library/aa287786(v=vs.71).aspx)
* declear default accessors
```csharp
public class Item
{
	// Use accessors if variable should be accessable from outside
	public string Name { get; set; } // name starts with capital letter
	public Item(string name)
	{
        Name = name;
    }
}
// Access data through set accessor
Item item = new Item("apple");
item.Name //=> "apple"
```


###### Condition
[More info](https://msdn.microsoft.com/en-us/library/676s4xab.aspx)
* if statement
```csharp
if (1 == 1)
{
    Console.WriteLine("case1");
}
else if (2 == 2)
{
    Console.WriteLine("case2");
}
else
{
    Console.WriteLine("case3");
}
end
```

* switch statement
```csharp
switch (1)
{
    case 1:
        Console.WriteLine("case1");
        break;
    case 2:
        Console.WriteLine("case2");
        break;
    default:
        Console.WriteLine("default");
        break;
}

```

###### String
[More info](https://msdn.microsoft.com/en-us/library/362314fe.aspx)
* create string
```csharp
string name = "apple";
```

* append string
```csharp
string name = "apple";
Console.WriteLine(name + " orange"); //=> apple orange
```

* combine strings
```csharp
string name = "apple";
Console.WriteLine("having {0}", name); //=> having apple
```

##### Array
[More info](https://msdn.microsoft.com/en-us/library/aa288453.aspx)

Array size can't be changed after initilized.

Values in Array can be changed.

* create array with data
```csharp
string[] array = new string[] {"apple", "orange"};
```

* get length
```csharp
string[] array = new string[] {"apple", "orange"};
array.Length; //=> 2
```

* change data
```csharp
string[] array = new string[] {"apple", "orange"};
array[0] = "jazz apple";
```

* get index
```csharp
string[] array = new string[] {"apple", "orange"};
Array.IndexOf(array, "apple"); // => 0
// Array class provides many methods used in List class
```

* shorten initializer
```csharp
// new string[] can be skipped in this case (won't work with 'var array')
string[] array = {"apple", "orange"};
```

##### List
[More info](https://msdn.microsoft.com/en-us/library/6sh2ey19(v=vs.110)
.aspx)
* create empty list
```csharp
List<string> list = new List<string>();
```

* create list with data
```csharp
List<string> list = new List<string>() {"apple", "orange"};
```

* add item to array
```csharp
List<string> list = new List<string>();
list.Add("milk");
```

* get item by index
```csharp
List<string> list = new List<string>() {"apple"};
list[0]; //=> "apple"
```

* find index of item
```csharp
List<string> list = new List<string>() {"apple", "orange"};
list.IndexOf("apple"); //=> 0
```

* loop through each item
```csharp
List<string> list = new List<string>() {"apple", "orange"};
foreach (string item in list)
{
	Console.WriteLine(item);
}
list.ForEach(item => Console.WriteLine(item)); // Short version
```

* map to create a new array with different value
```csharp
List<int> list = new List<int>() {1, 2, 3};
List<int> newList = list.Select(item => item * 3); //=> [3, 6, 9]
```

* Inject to compose data
```csharp
// update 0 5 times (0 -> 1 -> 3 -> 6 -> 10 -> 15) and return
List<int> list = new List<int>() {1, 2, 3, 4, 5};
int sum = list.Aggregate(0, (total, number) => total + number); //=> 15
```

##### Dictionary
[More info](https://msdn.microsoft.com/en-us/library/xfhwa508(v=vs.110).aspx)
* namespace
using System.Collections.Generic;

* create empty dictionary
```csharp
Dictionary<string, int> dic = new Dictionary<string, int>();
// key => string, value => int 
```

* add key and value pair
```csharp
Dictionary<string, int> dic = new Dictionary<string, int>();
dic.Add("key1", 100); // exception on exisiting key
dic["key1"] = 100;    // override value on existing key
```

* get value from key
```csharp
Dictionary<string, int> dic = new Dictionary<string, int>();
hash = {"key" => "value"}
hash["key"] #=> "value"
```

##### UnitTesting
* namespace
using Microsoft.VisualStudio.TestTools.UnitTesting;

* basic format
```csharp
[TestClass]
public class StoreTest // Test Store class
{
    [TestInitialize]
    public void InitWithSomething()
    {
    	// Called before each test
    }
    [TestMethod]
    public void TestSomething()
    {
        // Single test
        Assert.AreEqual(2, 1 + 1);
    }
}
```

* assert two value
```csharp
int expected = 2;
int actual = 1 + 1;
Assert.AreEqual(expected, actual);
```

* assert two lists
```csharp
var a = new List<int> {1, 2, 3};
var b = new List<int> {1, 2, 3};
CollectionAssert.Equals(a, b);
```