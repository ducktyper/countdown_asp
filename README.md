## Coding practice
Goal: Learn C# and ASP.NET MVC

We will build a self-checkout machine used in a supermarket together.

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