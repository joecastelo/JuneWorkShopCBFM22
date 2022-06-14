# JuneWorkShopCBFM22
Code for the June blog post about the varian ESAPI workshop at CBFM22
## Plan Check June 22 , 07/11, Fortaleza, Brazil 

Download the code, open the SLN or csproj in Visual Studio 2019
Reference the VMS.TPS libraries (if you are in TBOX)
C:\Program Files (x86)\Varian\RTM\15.6\esapi\API\VMS.TPS.Common.Model.Types.dll
C:\Program Files (x86)\Varian\RTM\15.6\esapi\API\VMS.TPS.Common.Model.API.dll
Compile it and go to Tools/Scripts, Open Folder looking for this file Solution folder bin/June22WorkShopPCheck.esapi.dll

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
