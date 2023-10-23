$(document).ready(function () {
    // Habilitar la funcionalidad de arrastre para las actividades
    $(".draggable-activity").draggable({
        revert: "invalid", // La actividad regresará a su posición original si no se suelta en el sprint
    });

    // Habilitar la funcionalidad de soltar en el sprint
    $("#sprint-container").droppable({
        accept: ".draggable-activity",
        drop: function (event, ui) {
            // Lógica cuando la actividad se suelta en el sprint
            const actividadId = ui.helper.attr("id"); // Obtén el ID de la actividad arrastrada
            const sprintId = "sprint-1"; // Puedes asignar un ID de sprint aquí

            // Agrega la lógica para asociar la actividad con el sprint (guardar en la base de datos, actualizar la vista, etc.)
            console.log(`Actividad ${actividadId} arrastrada al sprint ${sprintId}`);
        }
    });
});
