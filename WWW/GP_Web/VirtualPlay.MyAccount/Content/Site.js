//Capturar tamanho do navegador
var width = window.innerWidth;
var height = window.innerHeight;

//Variável de verificação
var check = document.getElementById("check");//div de verificação

//Barrra de navegação e menu
var dNavBar = document.getElementById("dNavBar");//Menu

//Menu ID
var mIdMob = document.getElementById("mIdMob");//Menu nome mobile
var mIdDsk = document.getElementById("mIdDsk");//Menu direito desktop

if (document.getElementById("check") == undefined) {
    var mHeight = height - 150;//Altura do menu mobile logado
}
else {
    var mHeight = height - 81;//Altura do menu mobile
}

//MOBILE
if (width < 768) {

    if (check == undefined) {
        dNavBar.style.marginTop = "70px";//Margem topo
    }

    //Menu
    dNavBar.style.maxHeight = mHeight.toString() + 'px';//Altura menu mobile
    dNavBar.className += " dNavBarMob";//Classe mobile botões do menu

    //Menu ID Mobile
    if (mIdMob != null) {
        if (check != undefined) {
            mIdDsk.style.display = "block";//Nome topo mobile some
            mIdDsk.style.marginTop = "-8px";//Margem topo
        }
        else {
            mIdMob.style.display = "block";//Menu nome desktop some
            document.getElementById("bLogoutMob").style.display = "block";//Botão sair
            document.getElementById("logoutForm").style.height = "0";
        }
    }

    //Index/Home
    var IndexMob = document.getElementById("jumbotron");
    var dIndexMob = document.getElementById("dIndexMob");
    if (dIndexMob != null && IndexMob != null) {
        dIndexMob.style.display = "block";//Menu direito
        IndexMob.style.marginLeft = "-24px";
        IndexMob.style.width = "calc(100% + 47px)";
    }

    //Mobile
    var mImgsMob = document.getElementsByTagName("img");//Iconeis do menu mobile

    //Iconeis do menu mobile
    if (check == undefined) var cont = 10;
    else var cont = 5;
    for (var i = 0; i <= cont; i++) {
        if (mImgsMob != null && mImgsMob[i].className == "mIconsMob imgpinpad") {
            mImgsMob[i].style.display = "block";
        }
    }
}

//DESKTOP
else {
    //Menu ID
    if (mIdDsk != null) {
        mIdDsk.style.display = "block";//Menu direito
    }

    //Classe desktop botões do menu
    if (check == undefined) {
        dNavBar.className += " dNavBarDsk";//Classe desktop botões do menu
        document.getElementById("css").setAttribute('href', "/VirtualPlay.MyAccount/Content/DskLogon.css");
    }

    //Index/Home
    var dIndexDsk = document.getElementById("dIndexDsk");
    if (dIndexDsk != null) {
        dIndexDsk.style.display = "block";
    }
    if (check == undefined) {
        document.getElementById("social_icons").style.display = "none";
    }
}