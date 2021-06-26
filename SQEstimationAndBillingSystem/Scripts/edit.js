var datatemp = [
    {
        "users": "1",
        "name": "Tiger Nixon"

    },
    {
        "users": "2",
        "name": "Garrett Winters"

    },
    {
        "users": "3",
        "name": "Ashton Cox"

    }];


var datatempChild = [
    {
        "users.first_name": "1",
        "users.last_name": "Tiger Nixon",
        "users.phone": "System Architect",
        "sites.name": "$320,800"
    },
    {
        "users.first_name": "2",
        "users.last_name": "Tiger Nixon",
        "users.phone": "System Architect",
        "sites.name": "$320,800"

    },
    {
        "users.first_name": "3",
        "users.last_name": "Tiger Nixon",
        "users.phone": "System Architect",
        "sites.name": "$320,800"
    }];
function createChild(row) {
	var rowData = row.data();

	// This is the table we'll convert into a DataTable
	var table = $('<table class="display" width="100%"/>');

	// Display it the child row
	row.child(table).show();

	// Editor definition for the child table
	var usersEditor = new $.fn.dataTable.Editor({
		ajax: {
			url: "/media/blog/2016-03-25/users.php",
			data: function(d) {
				d.site = rowData.id;
			}
		},
		table: table,
		fields: [
			{
				label: "First name:",
				name: "users.first_name"
			},
			{
				label: "Last name:",
				name: "users.last_name"
			},
			{
				label: "Phone #:",
				name: "users.phone"
			},
			{
				label: "Site:",
				name: "users.site",
				type: "select",
				placeholder: "Select a location",
				def: rowData.id
			}
		]
	});

	// Child row DataTable configuration, always passes the parent row's id to server
	var usersTable = table.DataTable({
		dom: "Bfrtip",
		pageLength: 5,
		//ajax: {
		//	url: "/media/blog/2016-03-25/users.php",
		//	type: "post",
		//	data: function(d) {
		//		d.site = rowData.id;
		//	}
        //},
        aaData: datatempChild,
		columns: [
			{ title: "First name", data: "users.first_name" },
			{ title: "Last name", data: "users.last_name" },
			{ title: "Phone #", data: "users.phone" },
			{ title: "Location", data: "sites.name" }
		],
		select: true,
		buttons: [
			{ extend: "create", editor: usersEditor },
			{ extend: "edit", editor: usersEditor },
			{ extend: "remove", editor: usersEditor }
		]
	});

	// On change, update the content of the parent table's host row
	// This isn't particularly efficient as it requires the child row
	// to be regenerated once the main table has been reloaded. A
	// better method would be to query the data for the new user counts
	// and update each existing row, but that is beyond the scope of
	// this post.
	usersEditor.on( 'submitSuccess', function (e, json, data, action) {
		row.ajax.reload(function () {
			$(row.cell( row.id(true), 0 ).node()).click();
		});
	} );
}

function updateChild(row) {
	$("table", row.child())
		.DataTable()
		.ajax.reload();
}

function destroyChild(row) {
	// Remove and destroy the DataTable in the child row
	var table = $("table", row.child());
	table.detach();
	table.DataTable().destroy();

	// And then hide the row
	row.child.hide();
}

$(document).ready(function () {

   

	var siteEditor = new $.fn.dataTable.Editor({
		ajax: "/media/blog/2016-03-25/sites.php",
		table: "#sites",
		fields: [
			{
				label: "Site name:",
				name: "name"
			}
		]
	});

	var siteTable = $("#sites").DataTable({
		dom: "Bfrtip",
		//ajax: "/media/blog/2016-03-25/sites.php",
        aaData: datatemp,
		order: [1, "asc"],
		columns: [
			{
				className: "details-control",
				orderable: false,
				data: null,
				defaultContent: "",
				width: "10%"
			},
			{ data: "name" },
			{
				data: "users",
				render: function(data) {
					return data.length;
				}
			}
		],
		select: {
			style: "os",
			selector: "td:not(:first-child)"
		},
		buttons: [
			{ extend: "create", editor: siteEditor },
			{ extend: "edit", editor: siteEditor },
			{ extend: "remove", editor: siteEditor }
		]
	});

	// Add event listener for opening and closing details
	$("#sites tbody").on("click", "td.details-control", function() {
		var tr = $(this).closest("tr");
		var row = siteTable.row(tr);

		if (row.child.isShown()) {
			// This row is already open - close it
			destroyChild(row);
			tr.removeClass("shown");
		} else {
			// Open this row
			createChild(row);
			tr.addClass("shown");
		}
	});

	// When updating a site label, we want to update the child table's site labels as well
	siteEditor.on("submitSuccess", function() {
		siteTable.rows().every(function() {
			if (this.child.isShown()) {
				updateChild(this);
			}
		});
	});
});