//İl Seçimi
$(document).ready(function () {
    $("#FaturaTuru").change(function () {
        if (this.checked) {
            $("#FaturaTuru").val("Kurumsal");
        }
        else {
            $("#FaturaTuru").val("Bireysel");
        }
    });


    $("#AdresEkle_UlkeId").change(function () {
        $("#kasabilgitopla").click(function () {
            if ($("#AdresEkle_IlId :selected").val() == 0) {
                alert("İl alanı boş bırakılamaz..!");
                return false;
            }
        });

        $("#AdresEkle_IlId").empty();
        $.ajax({
            type: 'POST',
            url: '/Home/IlleriGetir',
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            data: { id: $("#AdresEkle_UlkeId").val() },
            success: function (states) {
                console.log(states);
                $("#AdresEkle_IlId").append("<option value='0'>Seçiniz...</option>");
                $("#AdresEkle_IlId").prop("disabled", false);

                $.each(states, function (i, state) {
                    $("#AdresEkle_IlId").append('<option value="'
                        + state.Id + '">'
                        + state.IlAdi + '</option>');
                });
            },
            error: function (ex) {
                alert('Hata Oluştu' + ex.responseText);
            }
        });
        return false;
    })

    $("#AdresEkle_IlId").change(function () {
        $("#kasabilgitopla").click(function () {
            if ($("#AdresEkle_IlceId :selected").val() == 0) {
                alert("İl alanı boş bırakılamaz..!");
                return false;
            }
        });

        $("#AdresEkle_IlceId").empty();
        $.ajax({
            type: 'POST',
            url: '/Home/IlceleriGetir',
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            data: { id: $("#AdresEkle_IlId").val() },
            success: function (states) {

                $("#AdresEkle_IlceId").append("<option value='0'>Seçiniz...</option>");
                $("#AdresEkle_IlceId").prop("disabled", false);

                $.each(states, function (i, state) {

                    $("#AdresEkle_IlceId").append('<option value="'
                        + state.Id + '">'
                        + state.IlceAdi + '</option>');
                });
            },
            error: function (ex) {
                alert('Hata Oluştu' + ex.responseText);
            }
        });
        return false;
    })
});

$(document).ready(function () {
    $('.adres').change(function () {//Clicking input radio
        var radioClicked = $(this).attr('id');
        unclickRadio();
        removeActive();
        clickRadio(radioClicked);
        makeActive(radioClicked);
    });
    $(".card").click(function () {//Clicking the card
        var inputElement = $(this).find('input[type=radio]').attr('id');
        unclickRadio();
        removeActive();
        makeActive(inputElement);
        clickRadio(inputElement);
    });
});


function unclickRadio() {
    $(".adres").prop("checked", false);
}

function clickRadio(inputElement) {
    $("#" + inputElement).prop("checked", true);
}

function removeActive() {
    $(".card").removeClass("active");
}

function makeActive(element) {
    $("#" + element + "-card").addClass("active");
}
