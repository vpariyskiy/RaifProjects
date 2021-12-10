Feature: WorkCart

A short summary of the feature

@Cart
Scenario: Add Items to Cart
	Given EmptyCart
	When AddNewItems
	Then DeleteAllItems
