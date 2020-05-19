window.onload = load;

function load() {
    alert("load event detected!");
    let url = 'https://localhost:44383/api/TableOfProcesses/GetStatistics';
    let response = await fetch(url);

    let commits = await response.json(); // читаем ответ в формате JSON

    alert(commits[0].author.login);
}