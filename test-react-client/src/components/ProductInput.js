import React from "react";
import { Button, Input, Space, Form } from "antd";

export default function ProductInput({ onFinish }) {
  const [name, setName] = React.useState("");
  const [price, setPrice] = React.useState();
  const [quantity, setQuantity] = React.useState();

  return (
    <Space.Compact style={{ width: "100%" }}>
      <Input
        placeholder="name"
        value={name}
        onChange={(e) => setName(e.target.value)}
      />
        <Input
          placeholder="price"
          type="number"
          value={price}
          onChange={(e) => setPrice(e.target.value)}
        />
      <Input
        placeholder="quantity"
        type="number"
        value={quantity}
        onChange={(e) => setQuantity(e.target.value)}
      />
      <Button
        type="primary"
        onClick={() => onFinish({ name, price, quantity })}
      >
        Add
      </Button>
    </Space.Compact>
  );
}
