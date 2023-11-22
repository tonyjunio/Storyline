var bsMainOffCanvas = null;

function ToggleMainOffCanvas() {

    if (bsMainOffCanvas == null) {
        var mainOffCanvas = document.getElementById('mainOffCanvas');
        bsMainOffCanvas = new bootstrap.Offcanvas(mainOffCanvas);
    }

    bsMainOffCanvas.toggle();
}

let _TimelinePoints = [];
let _PeriodXyMap = [];
function RenderTimelineSummary() {
    let timelineSummary = document.getElementsByClassName("timeline-summary")[0];
    let periods = timelineSummary.querySelectorAll(".timeline-period");
    let events = timelineSummary.querySelectorAll(".event-item");
    let eventHeight = 24;
    let eventVerticalGap = 5;

    let tsWidth = timelineSummary.clientWidth;

    let timelinePoints = [];
    let distance = 0;
    periods.forEach((elem, idx) => {
        let elemWidth = elem.clientWidth;

        timelinePoints.push({
            year: elem.dataset.year,
            xIndex: idx,
            element: elem,
            width: elemWidth,
            startX: distance,
            endX: distance + elemWidth
        });

        distance += elemWidth;
    });
    console.log(timelinePoints);

    let eventPoints = [];
    let periodXyMap = [];
    events.forEach((elem) => {
        let eventId = elem.dataset.eventId;
        let startYear = elem.dataset.startYear;
        let endYear = elem.dataset.endYear;
        let startPoint = timelinePoints.find(point => point.year === startYear);
        let endPoint = timelinePoints.find(point => point.year === endYear);
        let eventWidth = endPoint.endX - startPoint.startX;

        let yIndex = 0;
        let yPosition1 = 0;
        let yPosition2 = 0;

        console.log("--------------------------------------------------");
        console.log(`seventId: ${eventId}`);
        console.log(`startPoint.xIndex: ${startPoint.xIndex}`);
        console.log(`endPoint.xIndex: ${endPoint.xIndex}`);
        console.log(`yIndex: ${yIndex}`);

        if (periodXyMap.length > 0) {
            let isOccupied = true;
            while (isOccupied) {
                for (let x = startPoint.xIndex; x <= endPoint.xIndex; x++) {
                    let mapItem = periodXyMap.find(item => {
                        return item.xIndex === x && item.yIndex === yIndex;
                    });

                    console.log(mapItem);

                    if (mapItem != undefined) {
                        yIndex = mapItem.yIndex + 1;
                        continue;
                    }

                    isOccupied = false;
                    break;
                }
            }
        }

        console.log("+++");
        console.log(periodXyMap);
        console.log("+++");

        eventPoints.push({
            startYear: startYear,
            endYear: endYear,
            startPointXIndex: startPoint.xIndex,
            endPointXIndex: endPoint.xIndex,
            startX: startPoint.startX,
            endX: endPoint.endX,
            width: eventWidth,
            yIndex: yIndex
        });

        // Set the event styles to show on the timeline summary.
        elem.style.width = eventWidth + 'px';
        elem.style.top = (yIndex * eventHeight) + (yIndex * eventVerticalGap) + 'px';
        elem.style.backgroundColor = rndRGBColorValue(colorElementMin = 95);

        // Add event (x,y) position.
        for (let x = startPoint.xIndex; x <= endPoint.xIndex; x++) {
            periodXyMap.push({
                xIndex: x,
                yIndex: yIndex,
                yPosition1: yPosition1,
                yPosition2: yPosition2,
                eventId: eventId
            });
        }
    });

    // Get all y values, then sort descendingly and finally get only the first value at first index.
    let highestY = periodXyMap.map((item) => item.yIndex).sort((a, b) => b - a)[0];

    // Set the timelineSummary height.
    timelineSummary.style.height = (highestY * eventHeight) + 70 + 'px';

    // Set debug variables.
    _TimelinePoints = timelinePoints;
    _PeriodXyMap = periodXyMap;

    console.log(eventPoints);
}

function rndRGBColorValue(colorElementMin = 0, colorElementMax = 255) {
    let min = colorElementMin;
    let max = colorElementMax;
    let fn = () => Math.floor(Math.random(0) * (max - min + 1)) + min;
    let r = fn(); g = fn(); b = fn();
    return `rgba(${r}, ${g}, ${b}, 1)`
}