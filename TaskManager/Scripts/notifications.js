$(function () {
    let token = $('input[name="__RequestVerificationToken"]');
    let unread = $('.unread');

    if (token.length && unread.length > 0) {
        $.ajax({
            url: '/Notifications/Read',
            method: 'POST',
            data: {
                __RequestVerificationToken: token.val()
            }
        })
    }
});
