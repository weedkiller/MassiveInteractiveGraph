$(function () {

    var findPathButton = $("#findPath");
    var userMessage = $("#userMessage");
    
    userMessage.html("you need to select 2 nodes");

    findPathButton.button("disable");
    findPathButton.click(function (event) {
        event.preventDefault();
        var selectedNodes = cy.$(".highlightedNode");
        if (selectedNodes.length == 2) {
            var sourceId = selectedNodes[0].data("id");
            var targetId = selectedNodes[1].data("id");

            sourceId = sourceId.replace("node_", "");
            targetId = targetId.replace("node_", "");

            var query = { sourceId: sourceId, targetId: targetId };

            $.getJSON("Graph/FindPath", query
            ).success(function (data, textStatus) {

                if (data.length == 0) {
                    alert("no route found");
                } else {

                    for (var i = 0; i < data.length - 1; i++) {
                        var id1 = data[i];
                        var id2 = data[i + 1];
                        cy.$("#link_" + id1 + "_" + id2).addClass('highlightedEdge');
                        cy.$("#link_" + id2 + "_" + id1).addClass('highlightedEdge');
                    }
                    $.each(data, function (i, item) {
                        cy.$("#node_" + item).addClass('highlightedNode');
                    });
                }

                userMessage.html("route found");

            }).error(function (data, textStatus) {

                userMessage.html("");
                alert("unexpected server error");

            });

        }
    });

    var serializedGraph = $("#serializedGraph").val();

    var g = $.parseJSON(serializedGraph);

    var cytoscapeNodes = [];
    var cytoscapeEdges = [];

    $.each(g.Nodes, function (i, item) {
        var node = { data: { id: "node_" + item.Id.toString(), label: item.Label } };
        cytoscapeNodes.push(node);
    });
    $.each(g.Links, function (i, item) {
        var edge = { data: { id: "link_" + item.Id1 + "_" + item.Id2, source: "node_" + item.Id1.toString(), target: "node_" + item.Id2.toString() } };
        cytoscapeEdges.push(edge);
    });

    var ns = JSON.stringify(cytoscapeNodes);
    var es = JSON.stringify(cytoscapeEdges);

    var cy = cytoscape({
        container: document.getElementById('cy'),

        style: cytoscape.stylesheet()
          .selector('node')
            .css({
                'content': 'data(label)'
            })
          .selector('edge')
            .css({
                'width': 4,
                'line-color': '#ddd',
            })
          .selector('.highlightedNode')
            .css({
                'background-color': '#61bffc',
                'line-color': '#61bffc',
                'transition-property': 'background-color, line-color',
                'transition-duration': '0.5s'
            })
          .selector('.highlightedEdge')
            .css({
                'background-color': '#61bffc',
                'line-color': '#61bffc',
                'transition-property': 'background-color, line-color',
                'transition-duration': '0.5s'
            }),

        elements: {
            nodes: cytoscapeNodes,
            edges: cytoscapeEdges
        },
        
        layout: {
            name: 'circle'
        }

        //layout: {
        //    name: 'breadthfirst',
        //    directed: false,
        //    roots: '#a',
        //    padding: 10
        //}
    });

    cy.on('tap', 'node', function (event) {

        userMessage.html("");

        if (cy.$(".highlightedEdge").length > 0) {
            cy.$(".highlightedEdge").removeClass('highlightedEdge');
            cy.$(".highlightedNode").removeClass('highlightedNode');
        }

        if (cy.$(".highlightedNode").length == 2) {
            cy.$(".highlightedNode").removeClass('highlightedNode');
        }

        var node = event.cyTarget;
        cy.$("#" + node.id()).addClass('highlightedNode');

        if (cy.$(".highlightedNode").length == 2) {
            findPathButton.button("enable");
        } else {
            findPathButton.button("disable");
        }

        if (cy.$(".highlightedNode").length == 1) {
            userMessage.html("you need to select 2 nodes");
        }
    });
});