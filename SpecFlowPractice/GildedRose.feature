Feature: Gilded Rose
	In order to track items state
	As a shop owner
	I want tool to calculate item quality with each passing day

@unit
Scenario: Item quality degrades and sell in date degrades with each passing date
	Given I have the following items in stock
	| Name    | Quality | Sell In |
	| Item #1 | 10      | 4       |
	| Item #2 | 7       | 2       |
	When 1 day(s) pass
	Then I have the following items in the stock
	| Name    | Quality | Sell In |
	| Item #1 | 9       | 3       |
	| Item #2 | 6       | 1       |

Scenario Outline: Processing items state in shop
	Given I have item <Name> with quality <Quality> and Sell In days <Sell In>
	When <Days> day(s) pass
	Then I get quality <Result Quality> 

	Examples: 
	| Name                       | Quality | Sell In | Days | Result Quality |
	| Item #1                    | 6       | 3       | 1    | 5              |
	| Item #2                    | 7       | 5       | 2    | 5              |
	| Item #3                    | 5       | 0       | 2    | 1              |
	| Sulfuras, Hand of Ragnaros | 8       | 6       | 8    | 8              |
