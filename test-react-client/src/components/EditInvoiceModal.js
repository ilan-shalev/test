import React, { useState } from "react";
import { Button, Input, Modal, List, Typography } from "antd";
import { axiosInstance } from "../axios";
import styled from "styled-components";
import ProductInput from "./ProductInput";

export default function EditInvoiceModal({
  show,
  onFinish,
  onCancel,
  invoice,
}) {
  const [editedInvoice, setEditedInvoice] = useState(invoice);
  const [showAddProduct, setShowAddProduct] = useState(false);

  const updateInvoice = () => {
    onFinish(editedInvoice);
    setEditedInvoice({});
    setShowAddProduct(false);
  };

  return (
    <ModalForm
      open={show}
      onCancel={() => {
        setEditedInvoice({});
        setShowAddProduct(false);
        onCancel();
      }}
      onOk={updateInvoice}
    >
      <Input
        placeholder="Recipient"
        value={editedInvoice.recipient}
        onChange={(e) =>
          setEditedInvoice((prev) => {
            return { ...prev, recipient: e.target.value };
          })
        }
      />
      <List
        header={<div>products</div>}
        // footer={<div>Footer</div>}
        bordered
        dataSource={editedInvoice.products}
        renderItem={(product) => (
          <List.Item>
            <Input
              strong
              defaultValue={product.name}
              placeholder="name"
              onChange={(e) => {
                product.name = e.target.value;
                setEditedInvoice((p) => {
                  return {
                    ...p,
                    products: [...p.products.filter((x) => x.id !== product.id), product],
                  };
                });
              }}
            />
            <Input
              strong
              defaultValue={product.price}
              placeholder="price"
              onChange={(e) => {
                product.price = e.target.value;
                setEditedInvoice((p) => {
                  return {
                    ...p,
                    products: [...p.products.filter((x) => x.id !== product.id), product],
                  };
                });
              }}
            />
            <Input
              strong
              defaultValue={product.quantity}
              placeholder="quantity"
              onChange={(e) => {
                product.quantity = e.target.value;
                setEditedInvoice((p) => {
                  return {
                    ...p,
                    products: [...p.products.filter((x) => x.id !== product.id), product],
                  };
                });
              }}
            />
          </List.Item>
        )}
      />
      {showAddProduct && (
        <ProductInput
          onFinish={(product) => {
            setShowAddProduct(false);
            setEditedInvoice((prev) => {
              return { ...prev, products: [...(prev.products ?? []), product] };
            });
          }}
        />
      )}
      {!showAddProduct && (
        <Button onClick={() => setShowAddProduct(true)}>Add Product</Button>
      )}
    </ModalForm>
  );
}

const ModalForm = styled(Modal)`
  display: flex;
  flex-direction: column;
  gap: 4rem;
`;
