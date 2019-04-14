$(function main() {
	(function displayContent() {
		const url = document.URL;
		var anchor = url.substring(url.indexOf("=")+1);
		anchor = ((anchor == "" || anchor == null || anchor == undefined || anchor == url) ? "index" : anchor);
		$("title").text(function() {
			return "PEA - " + (
				$(".main #" + anchor + " h2:first-child").text() == "Introduction" ? 
				"Home" : $(".main #" + anchor + " h2:first-child").text()
			); 
		});
		
		// $.get("bin/" + anchor + ".xml").done(data => console.log(data)).fail(err => console.log(err));
		$(".main #" + anchor).show();		
		
		$.each(document.querySelectorAll("#banner-bottom * a"), (key, val) => {
			if (val.className == "") 
				val.addEventListener("click", ()=>{ location.reload(); });
		});
	})();
	
	(function() {
		function checkResize() {
			$("#pea").text($(window).width() <= 768 ? "PEA - Philip Dumaresq" : "Program Exit Assessment");
			
			if ($(window).width() <= 767) {
				$("#banner-navbar").addClass("navbar-fixed-top"); 
				$("#banner-bottom").addClass("change");
				imgLogo.height = imgLogo.width = 25;
			}
			else {
				$("#banner-navbar").removeClass("navbar-fixed-top"); 
				$("#banner-bottom").removeClass("change");
				imgLogo.height = imgLogo.width = 50;
			}
		}
		
		checkResize();
		$(window).resize(() => checkResize());
	})();
	
	(function setYear() {
		let dates = [].slice.call(document.querySelectorAll(".currYear"));
		dates.map(d => {
			d.innerText = (new Date()).getFullYear();
			return d;
		});
	})();
	
	$("#imgTop").click(() => $("html, body").animate({ scrollTop: 0 }, "medium"));
	
	$(document).scroll(function() {
		if ($("#banner-bottom").is(":visible")) 
			$("#banner-bottom").addClass("navbar-hide") 
				&& document.getElementById("navbar-burger").classList.remove("change") 
				|| $("#banner-bottom").removeClass("change");
	});
	
	$("html").click(e => {		
		let inNavBar = false, currNode = e.target;
		while (inNavBar == false) {
			inNavBar = currNode.id == "banner-bottom";
			
			if (currNode.parent == undefined) {
				break;
			}
			currNode = currNode.parent;
		}
		
		if (e.target.id == "navbar-burger" || (e.target.parent != undefined && e.target.parent.id == "navbar-burger")) {
			document.getElementById("navbar-burger").classList.toggle("change");
		
			if ($("#banner-bottom").is(":visible")) {
				$("#banner-bottom").addClass("navbar-hide").removeClass("change");
			}
			else {
				$("#banner-bottom").removeClass("navbar-hide").addClass("change");
			}
		}
		else if (!inNavBar) {
			if ($("#banner-bottom").is(":visible")) {
				document.getElementById("navbar-burger").classList.toggle("change");
				$("#banner-bottom").addClass("navbar-hide").removeClass("change");
			}			
		}
	});
});