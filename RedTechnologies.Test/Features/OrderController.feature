Feature: OrderController

@addneworder
Scenario: When user asked to create a new order, it should return success
When asked to create a new order
Then should return success

@updateaorder
Scenario: When user asked to update a order, it should return the order updated
When asked to update a order
Then should return success

@getallorders
Scenario: When user asked to get all orders, it should return a order list
When asked to get all orders
Then should return success a order list

@deleteaorder
Scenario: When user asked to delete a order, it should return success
When asked to delete a order
Then should return success

@getorberbytype
Scenario: When user asked to get a order by type, it should return success not null
When asked to get a order by type
Then should return a order list with the type required

@getorbersbycustomer
Scenario: When user asked to get orders by customer, it should return a order list from customer required
When asked to get orders by customer
Then should return a order list from customer required