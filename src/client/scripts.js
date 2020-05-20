async function loadStatistics() {    
    let url = 'http://localhost:50253/api/tableofprocesses/statistics';
    let response = await fetch(url);
    let statistics = await response.json();    
    let nonpagedSystemMemorySizeHeader = document.getElementById('nonpaged-system-memory-size');
    let pagedMemorySizeInBytesHeader = document.getElementById('paged-memory-size');    
    nonpagedSystemMemorySizeHeader.innerText = statistics.sumNonpagedSystemMemorySize64InBytes;
    pagedMemorySizeInBytesHeader.innerText = statistics.sumPagedMemorySize64InBytes;

    let tableOfProcessesBody = document.getElementById('table-of-processes-body');
    refreshTableData(tableOfProcessesBody, statistics.processes);
}

function refreshTableData(tableBody, data) {
    let html = '';
    for (let element of data) {
        html += `<tr>
                    <td>${element.pid}</td>
                    <td>${element.command}</td>
                    <td>${element.nonpagedSystemMemorySize64InBytes}</td>
                    <td>${element.pagedMemorySize64InBytes}</td>
                    <td>${element.workingSet64InBytes}</td>
                    <td>${element.totalProcessorTimeInMilliseconds}</td>
                </tr>`;
    }
    tableBody.innerHTML = html;
}

function showAlarm(message) {
    var alarmBlock = document.getElementById('alarm-block');
    alarmBlock.innerText = message;
}

async function notificationSubscribe() {
    let url = 'http://localhost:50253/api/tableofprocesses/notifyme';
    let response = await fetch(url);
    if (response.status == 502) {
        showAlarm('');
        await notificationSubscribe();
    } else if (response.status == 204) {
        showAlarm('');
        await notificationSubscribe();
    } else if (response.status != 200) {        
        showAlarm(response.statusText);        
        await new Promise(resolve => setTimeout(resolve, 1000));
        await notificationSubscribe();
    } else {        
        let message = await response.text();       
        showAlarm(message);        
        await notificationSubscribe();
    }
}

loadStatistics();
let timer = setInterval(loadStatistics, 500);
notificationSubscribe();
