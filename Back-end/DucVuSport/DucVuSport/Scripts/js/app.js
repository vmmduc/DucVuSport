<script>
    $('.add-to-cart').each(function () {
        $(this).click(function () {
            var model = {};
            model.id = $(this).attr('id');
            model.quantity = 1;

            $.ajax({
                type: "POST",
                url: '@Url.Action("AddToCart", "Cart")',
                data: JSON.stringify(model),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function () {
                    location.reload();
                    alert("Thêm thành công!")
                }
            });
        });
    })
</script>