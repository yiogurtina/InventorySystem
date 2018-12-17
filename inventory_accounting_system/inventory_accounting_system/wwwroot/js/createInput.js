            var selectText = document.getElementById("onChangeText");
            var selectedText = selectText.options[selectText.selectedIndex].value;
            console.log(selectedText);
            if (selectedText === "3b7a65bf-7ca3-456c-a703-52c104d5ad76") {
                var input = document.createElement('input');
                var label = document.createElement('label');

                label.style.marginTop = ".15in";
                label.setAttribute("id", "labelId");

                input.className = "form-control";
                input.setAttribute("id", "inputId");
                input.setAttribute("name", "serialNum");
                document.getElementById('catSelect').appendChild(label);
                document.getElementById('catSelect').appendChild(input);

                document.getElementById("labelId").innerHTML = "Серийный номер";

            } else {
                var input = document.getElementById("inputId");
                var label = document.getElementById("labelId");
                if (input != null) {
                    input.parentNode.removeChild(input);
                }
                if (label != null) {
                    label.parentNode.removeChild(label)
                }
            }
