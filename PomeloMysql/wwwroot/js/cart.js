

function SepetGuncelle() {
    $.ajax({
        url: '/Sepet/_Sepet/',
        type: "get",
        success: function (result) {
            $("#UrunSayisi").html("("+result+")");

        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert("Hata Oluştu");
        },
        beforeSend: function () {
        },
        complete: function () {

        }
    });
}



function SepeteEkle(UrunId, adet) {
    var PostData = {
        UrunId: UrunId,
        Adet: adet,
    };
    $.ajax({
        url: '/Sepet/AddToCart',
        data: PostData,
        dataType: "json",
        type: "POST",
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        success: function (result) {
            if (result == true) {
                SepetGuncelle();
            }
            else {

            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert("Hata Oluştu");
        },
        beforeSend: function () {
        },
        complete: function () {

        }
    });


}



function SepetUrunSil(UrunId) {

    $.ajax({
        url: '/Sepet/RemoveCart?UrunId=' + UrunId,
        data: { UrunId: UrunId },
        dataType: "json",
        type: "POST",
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        success: function (result)
        {
            if (result == true)
            {
                SepetGuncelle();
            }
            else
            {

            }
        },
        error: function (jqXHR, textStatus, errorThrown)
        {
            alert(textStatus);
        },
        beforeSend: function ()
        {
        },
        complete: function ()
        {

        }
    });

}


function AlisverisListemeEkle(UrunId) {
    var PostData = {
        UrunId: UrunId,
    };
    $.ajax({
        url: '/Urunler/AlisverisListemeEkle',
        data: PostData,
        dataType: "json",
        type: "POST",
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        success: function (result) {
            
            $("#Display").css("display", result.display);
            $("#yorumSonuc").html(result.message);
            $(".Status").addClass(result.status);
            $("#Yorum").val("");
        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert(textStatus);
        },
        beforeSend: function () {
        },
        complete: function () {

        }
    });


}

function AlisverisListemUrunSil(UrunId) {

    $.ajax({
        url: '/Urunler/AlisverisListeUrunSil?UrunId=' + UrunId,
        data: { UrunId: UrunId },
        dataType: "json",
        type: "POST",
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        success: function (result) {
            if (result == true) {
                $('#alisverislistem-' + UrunId).remove();
            }
            else {

            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            alert(textStatus);
        },
        beforeSend: function () {
        },
        complete: function () {

        }
    });

}