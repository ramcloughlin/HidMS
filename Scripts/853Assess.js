function MLRQ1Change(value) {
    var val = value;
   
    if (value == "Yes") {
       
        $('#MLRQ2Div').show();
        $('#GeneralHygieneStatus').val("Regulation 853 applies unless there is exemption");
    }

    else if (value != "Yes") {
        
        
        $('#GeneralHygieneStatus').val("Regulation (EC) 852 only applies");
        $('#MLRQ2').val("");
        $('#MLRQ2Div').hide();
        $('#MLRQ3Div').hide();
        $('#MLRQ4Div').hide();
        $('#MLRQ5Div').hide();
    }
}
function MLRQ2Change(value) {
    var val = value;
   
    if (value == "Yes") {

        $('#MLRQ3Div').hide();
        $('#MLRQ4Div').hide();
        $('#MLRQ5Div').hide();
        $('#GeneralHygieneStatus').val("Regulation (EC) 852 only applies and temperature Requirements of Regulaiton 853");
    }

    else if (value != "NO") {


        $('#GeneralHygieneStatus').val("No Exemption Please continue");
        $('#MLRQ3').val("");
        $('#MLRQ3Div').show();
    }
}
function MLRQ3Change(value) {
    var val = value;
    
    if (value == "Yes") {

        $('#MLRQ4Div').hide();
        $('#MLRQ5Div').hide();
        $('#GeneralHygieneStatus').val("Regulation (EC) 852 only applies and temperature Requirements of Regulaiton 853");
    }

    else if (value != "NO") {


        $('#GeneralHygieneStatus').val("No Exemption Please continue");
        $('#MLRQ4').val("");
        $('#MLRQ4Div').show();
    }
}
function MLRQ4Change(value) {
    var val = value;

    if (value == "Yes") {

        
        $('#MLRQ5Div').show();
        $('#GeneralHygieneStatus').val("Such supply may be exempt under MLR Criteria");
    }

    else if (value != "NO") {


        $('#GeneralHygieneStatus').val("Regulation 853 Applies");
        $('#MLRQ5').val("");
        $('#MLRQ5Div').hide();
    }
}
function MLRQ5Change(value) {
    var val = value;

    if (value == "Yes") {


       
        $('#GeneralHygieneStatus').val("Regulation (EC) 852 only applies");
    }

    else if (value != "NO") {


        $('#GeneralHygieneStatus').val("Regulation 853 Applies");
        
    }
}