Feature: StockPreiction
       getting and preparing training data  
 
@mytag
Scenario Outline: creating traning data
	Given I have stock <symbol>
	And I have <DataProvider> for the stock symbol
	When I get data for the stock
	Then The data should be in a suitable <format> for training

Examples: 
| symbol | DataProvider | format  |
| "AMZN" | "csv"        | "Stock" |



@mytag
Scenario Outline: creating a model
	Given I have <trainingData> of <symbol>  
	And I have <trainingMethod>
	When train the data
	Then I should get a <Model>

Examples: 
| trainingData | symbol | trainingMethod     | Model   |
| "Stock"      | "AMZN" | "LeaniarRegretion" | "Model" |
