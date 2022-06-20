$(document).ready(function () {
    document.querySelectorAll('.sidebar-submenu').forEach(e => {
        e.querySelector('.sidebar-menu-dropdown').onclick = (event) => {
            event.preventDefault()
            e.querySelector('.sidebar-menu-dropdown .dropdown-icon').classList.toggle('active')

            let dropdown_content = e.querySelector('.sidebar-menu-dropdown-content')
            let dropdown_content_lis = dropdown_content.querySelectorAll('li')

            let active_height = dropdown_content_lis[0].clientHeight * dropdown_content_lis.length

            dropdown_content.classList.toggle('active')

            dropdown_content.style.height = dropdown_content.classList.contains('active') ? active_height + 'px' : '0'
        }
    })

    $.ajax({
        url: "/Admin/HomeAdmin/GetInfor",
        dataType: "json",
        success: function (response) {
            let category_options = {
                series: [response.tpbs, response.dctl, response.tltt, response.hpcn],
                labels: ['TPBS', 'DCTL', 'TLTT', 'PHCN'],
                chart: {
                    type: 'donut',
                },
                colors: ['#6ab04c', '#2980b9', '#f39c12', '#d35400']
            }

            let category_chart = new ApexCharts(document.querySelector("#category-chart"), category_options)
            category_chart.render()
        }
    })
})