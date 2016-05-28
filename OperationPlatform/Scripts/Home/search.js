function SearchClick() {
    var value = $("#seachCompany_selectColmunValue").val();

    if ($.trim(value).length > 0) {
        var Q = "searchStr=" + value;
        window.open("/ShopList/index?" + Q);
    } else {
        alert("请输入你要搜索的值");
    }
}