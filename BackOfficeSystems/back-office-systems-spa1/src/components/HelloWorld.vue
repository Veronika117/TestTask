<template>
  <div class="hello">
    <b-table ref="table" striped hover :items="items" :fields="fields" v-on:refresh="getBrands">
       <template slot-scope="props">
                <b-table-column field="quantity" label="Quantity" sortable>
                    {{ props.row.quantity }}
                </b-table-column>
              <b-table-column field="brandName" label="Brand Name" sortable>
                    {{ props.row.brandName }}
                </b-table-column>
            </template>
    </b-table>

    <b-button v-if="!addingBrandToggleLocal" v-on:click="addingBrandToggleLocal = !addingBrandToggleLocal" variant="outline-primary"  >Add Brand</b-button> 
  <b-form inline v-if="addingBrandToggleLocal">
    <label class="sr-only" for="inline-form-input-name">Name</label>
    <b-input
      id="inline-form-input-name"
      v-model="brandToAddLocal"
      class="mb-2 mr-sm-2 mb-sm-0"
      placeholder="brand name"
    ></b-input>
    <b-button v-on:click="addBrand" variant="primary">Add</b-button>
  </b-form>

    <b-button  v-if="!showLoadOrdersForm" v-on:click="showLoadOrdersForm = !showLoadOrdersForm" variant="outline-primary" >Load orders from file</b-button>
     <b-form inline v-if="showLoadOrdersForm">
    <label class="sr-only" for="inline-form-input-name">Path to file:</label>
    <b-input
      id="inline-form-input-name"
      v-model="brandToAddLocal"
      class="mb-2 mr-sm-2 mb-sm-0"
      placeholder="path"
    ></b-input>
    <b-button v-on:click="loadFile" variant="primary">Load</b-button>
  </b-form>

  </div>
</template>

<script>
export default {
  name: 'HelloWorld',
  props: {
  },
  
   data() {
      return {
        sortBy: 'quantity',
        sortDesc: false,
        fields: [
          { key: 'brandName', sortable: true },
          { key: 'quantity', sortable: true },],
        items: [],
        brandToAddLocal: this.brandToAdd,
        addingBrandToggleLocal: this.addingBrandToggle,
        showLoadOrdersForm: false,
        path: ''
      }
    },
    methods: {
       loadFile() {
         const axios = require('axios').default;
        var dto = { filePath: this.path };
         axios.post('http://localhost:5000/api/orders/file', dto)
         .then(() => {
           this.getBrands();
           this.showLoadOrdersForm = false;
            this.$refs.table.refresh();
           })
      },
      addBrand: function() {
        const axios = require('axios').default;
        var brands = [ { name: this.brandToAddLocal}];
         axios.post('http://localhost:5000/api/brands', brands)
         .then(() => {
           this.getBrands();
           this.addingBrandToggleLocal = false;
            this.$refs.table.refresh();
           })
      },
      getBrands: function() {
      const axios = require('axios').default;
      axios.get('http://localhost:5000/api/orders/quantity')
      .then(response => this.items = response.data);
      },},
       created() {
          this.getBrands();
      },
      
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h3 {
  margin: 40px 0 0;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
</style>
