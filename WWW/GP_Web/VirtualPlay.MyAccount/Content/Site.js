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

//Botão Nome/ID
var mPsIdDsk = document.getElementById("mPsIdDsk");//Botão N
var mPsIdMob = document.getElementById("mPsIdMob");//Botão Nome/ID Mob

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
            mIdMob.style.display = "none";//Nome topo mobile some
            mIdDsk.style.marginTop = "-8px";//Margem topo
            mPsIdMob.style.display = "none";//Botão Nome/Id mobile some
        }
        else {
            mIdDsk.style.display = "none";//Menu nome desktop some
            document.getElementById("bLogoutMob").style.display = "block";//Botão sair
            mPsIdDsk.style.display = "none";//Botão Nome/Id desktop some
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
        if (mImgsMob != null && mImgsMob[i].className == "mIconsMob") {
            mImgsMob[i].style.display = "block";
        }
    }
}

//DESKTOP
else {
    if (check == undefined) {
        dNavBar.className += " dNavBarDsk";//Classe mobile botões do menu
        document.getElementById("css").setAttribute('href', "/VirtualPlay.MyAccount/Content/DskLogon.css");
    }
    //Menu ID
    if (mIdDsk != null) {
        mIdDsk.style.display = "block";//Menu direito
        mPsIdMob.style.display = "none";//Botão Nome/Id mobile some
    }

    //Index/Home
    var dIndexDsk = document.getElementById("dIndexDsk");
    if (dIndexDsk != null) {
        dIndexDsk.style.display = "block";
    }
}