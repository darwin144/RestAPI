
const openArticle = document.getElementById("start");
openArticle.addEventListener('click', function() {
    const itemSatu = document.getElementById("right");
    itemSatu.style.backgroundColor = "light blue";
    itemSatu.style.color = "white";
    const content = document.createElement('p');
    const teksContent = document.createTextNode("Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum. It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English. Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy.Various versions have evolved over the years, sometimes by accident, sometimes on purpose (injected humour and the like).");
    content.append(teksContent);
    itemSatu.replaceChildren(content);
})

// mouse over
let closeArticle = document.getElementById("right");
closeArticle.addEventListener("mouseenter", function () {
    closeArticle.style.backgroundColor = "lightslategrey";
})

closeArticle.addEventListener("mouseleave", function () {
    closeArticle.style.backgroundColor = "grey";
})

const doubleClick = document.getElementById("changes-background");
doubleClick.addEventListener("dblclick", function () {
    const articleTwo = document.getElementById("row-up");
    articleTwo.style.backgroundColor = "lightseagreen";
    articleTwo.style.fontWeight = 400;    
})


const tombol3 = document.getElementById("tombolThree");
tombol3.addEventListener('click', function () {
    const articleThree = document.getElementById("row-down");
    articleThree.innerHTML = "Thank you";

})

const animals = [
    { name: "nemo", species: "fish", class: { name: "invertebrata" } },
    { name: "gary", species: "mouse", class: { name: "mamalia" } },
    { name: "dory", species: "fish", class: { name: "invertebrata" } },
    { name: "tom", species: "mouse", class: { name: "mamalia" } },
    { name: "aji", species: "wibu", class: { name: "mamalia" } }
]

let onlyMouse = [];
for (let i = 0; i < animals.length; i++) {
    
    if (animals[i].class.name === "mamalia" && animals[i].species !== "mouse") {
        animals[i].class.name = "non mamalia";
    }

    if (animals[i].species === "mouse") {
        onlyMouse.push(animals[i])
    }
}
console.log(onlyMouse);
console.log(animals);

// cara ke dua
const onlyMose = animals.filter(animal => animal.species = "mouse");
//

