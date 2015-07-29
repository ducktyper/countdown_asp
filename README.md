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