# JuneWorkShopCBFM22
Code for the June blog post about the varian ESAPI workshop at CBFM22
## Plan Check June 22 , 07/11, Fortaleza, Brazil 

### João Henrique Castelo and Anselmo Mancini 

#### We need 5 checks

	[X]	- Do not Run the check if Dose is not valid
	[X] - Do not Run the check if there is no RT Prescription
	[X] - Check if Dicom Origin was changed by an user
		How ? 
		[X] - Origin is 0,0,0 if not changed
	[X] - Check if there is a Setup Field in the Plan
		How ?
		[X] - Check Setup property in the field
	[X] - Check if MU Factor is larger than 7
		How ?
		[X] - Sum all fields MUs and divide it by the dose per fraction in cGy
	[X] - Check the primary reference point dose 
		How ?
		[X] Check Prescription and Dose Information
		[X] Dose per day and per session = Total Dose/ nbOfFractions
		[X] Rounding is important here
	[] - Match PTV D95 with Doctor prescription /// HomeWork ? 😎
		How ?
		[] Loop on RT Prescription
		[] Loop PTV List
		[] Check if D95 is within Prescription 90-100% 
		[] Match PTVs and report to the User 🚀

#### Second Part, User Interface 
	[X] - Create a screen for a single Check
	[X] - Translate from the mind to code 
	[]	- 		
